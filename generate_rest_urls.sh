#!/bin/bash

# This scripts scans the elasticsearch source code for all the registered REST endpoints 
# It will put the formatted output in $DEFINITIONOUTPUTFILE 
# [MethodName] [HttpVerb] [Route]

ESFOLDER="../elasticsearch"
DEFINITIONOUTPUTFILE="src/Generated/rest-actions.txt"

# Find all the lines that registerHandlers
grep -r "controller.registerHandler" ${ESFOLDER}/src/main/java/org/elasticsearch/rest/ |
    # Nodestats has some dynamic route registration going on
	sed -e 's/" + flag.getRestName() + "/{flags}/g' |
	sed -e 's/" + flag.getRestName()/{flags}"/g' | #can also happen at the end
	# Rip out the interesting parts of those lines
	sed -e 's/^.*\/Rest\(.*\)Action.java\:.*controller.registerHandler(\([a-zA-Z.]*\), "\([^"]*\)".*$/\1 \2 \3/p' | 
	# Some registrations use the fully qualified enum name, lets get rid of those
	sed -e 's/RestRequest.Method.//g'|
	sed -e 's/(?! )\/$//g' | #remove any trailing slashes unless its the root
	sed -e 's/ _stats\/docs/ \/_stats\/docs/g' | #the only route not beginning with a /
	sort | #sort all the registrations
	uniq | #remove duplicate lines
	# Give IndicesStats a more descriptive method name
	sed -e 's/IndicesStats GET \/_stats\/\(.\)\([^\/]*\)/IndicesStat\U\1\E\2 GET \/_stats\/\1\2/g' |
	sed -e 's/IndicesStats GET \/{index}\/_stats\/\(.\)\([^\/]*\)/IndexFilteredStats\U\1\E\2 GET \/{index}\/_stats\/\1\2/g' |
	# Nodestats duplicate urls for the same handlers see:
	# https://github.com/elasticsearch/elasticsearch/blob/master/src/main/java/org/elasticsearch/rest/action/admin/cluster/node/stats/RestNodesStatsAction.java
	# So we remove the ones we dont need
	sed -e 's/NodesStats GET \/_nodes\/.*\/stats$//g' | 
	sed -e 's/NodesStats GET \/_nodes\/stats$//g' |
	# Alter the _cluster/nodes/stats routes into the /_nodes/stats
	sed -e 's/NodesStats GET \/_cluster\/nodes\(.*\)$/NodesStats GET \/_nodes\1/g' |
	# Give the now dedup'ed notestats routes a more descriptive method names based on their url
	sed -e 's/NodesStats GET \/_nodes\/stats\/\(.\)\([^\/]*\)/NodesStats\U\1\E\2 GET \/_nodes\/stats\/\1\2/g' |
	sed -e 's/NodesStats GET \/_nodes\/{nodeId}\/stats\/\(.\)\([^\/]*\)/NodeStats\U\1\E\2 GET \/_nodes\/{nodeId}\/stats\/\1\2/g' |

	# Remove the route duplicates for NodesInfo and NodesHotThreads
	sed -e 's/NodesHotThreads GET \/_cluster.*$//g' |
	sed -e 's/NodesHotThreads GET .*hotthreads$//g' |
	sed -e 's/NodesInfo GET \/_cluster.*$//g' |

	# Remove the duplicate for delete mapping
	sed -e 's/DeleteMapping DELETE \/{index}\/{type}$//g' |

	# Remove the duplicate cluster shutdown
	sed -e 's/^.*\/_cluster\/nodes\/_shutdown$//g' |

	# HEAD on Source need a unique method name
	sed -e 's/^Head HEAD \/{index}\/{type}\/{id}\/_source$/HeadSource HEAD \/{index}\/{type}\/{id}\/_source/g' |

	# _create Urls need to be renamed from Index to Create
	sed -e 's/^\w* \(.*\)\/_create$/Create \1\/_create/g' |

	# Alias methods are slightly ambiguous too
	sed -e 's/^IndexPutAlias PUT \/_alias\/{name}$/IndexPutAliasByName PUT \/_alias\/{name}/g' |
	sed -e 's/^IndexPutAlias PUT \/_alias\/{name}$/IndexPutAliasByName PUT \/_alias\/{name}/g' |

	# Give the NodesInfo routes a better method name based on their url
	sed -e 's/NodesInfo GET \/_nodes\/{nodeId}\/\(.\)\([^\/]*\)/NodeInfo\U\1\E\2 GET \/_nodes\/{nodeId}\/\1\2/g' |
	sed -e 's/NodesInfo GET \/_nodes\/\(.\)\([^\/]*\)/NodesInfo\U\1\E\2 GET \/_nodes\/\1\2/g' |
	sed -e 's/NodesInfo{nodeId} GET/NodesInfo GET/g' |

	# Percolate count endpoint needs a unique method name
	sed -e 's/Percolate \(.*\)\/count/PercolateCount \1\/count/g' |
	# Remove blank lines
	sed -e '/^$/d' |
	# Sort and make sure all the lines are unique again
	sort | uniq > $DEFINITIONOUTPUTFILE 


# Generate intermediate definition file  
cat src/Generated/rest-actions.txt | 
	sed -e 's/\([^ ]*\) \([^ ]*\) \(.*\)$/\/\/\/<summary>\2 \3<\/summary>\nConnectionStatus \1\2(\3);\n\n\/\/\/<summary>\2 \3<\/summary>\nTask<ConnectionStatus> \1\2Async(\3);\n/' |
	cat > src/Generated/intermediate-definition.txt

# Patch defintion file so url params become method params
echo "" > src/Generated/intermediate-definition-params.txt
cat src/Generated/intermediate-definition.txt | while read LINE ; do
	N=$((N+1))

	if [[ $LINE = *ConnectionStatus* ]]
	then
		echo "$LINE" | 
			sed -e 's/\/{/, string /g' | #replace /{ with string param
		    sed -e 's/}\([^,]*\)//g' | #remove } up to first , or )
			sed -e 's/(, string/(string/'|  #touch up first param
            sed -e 's/\/[^,]*//g' | #remove urls bit from params.
            if [[ $LINE = *GET* ]] 
            then
            	sed -e 's/$/, NameValueCollection queryString = null);/'
			else
            	sed -e 's/$/, object body, NameValueCollection queryString = null);/'
			fi | #do not render a body parameter for GET calls
			sed -e 's/(, /(/g' | #remove any empty first params

			#Touching up the methodnames as best as we can
			sed -e ' s/\(Get\w*\)GET/\1/g' | #remove starting Get if GET is already in the method name
			sed -e ' s/\(Delete\w*\)DELETE/\1/g' | #remove DELETE if Delete is already in the method name (flows better)
			sed -e ' s/\(Head\w*\)HEAD/\1/g' | #remove HEAD if Head is already in the method name (flows better)
			sed -e ' s/\(Put\w*\)PUT/\1/g' | #remove PUT if Put is already in the method name (flows better)
			sed -e ' s/GET/Get/g' | #replace GET with Get
			sed -e ' s/HEAD/Head/g' | #replace HEAD with Head
			sed -e ' s/PUT/Put/g' | #replace PUT with Put
			sed -e ' s/POST/Post/g' | #replace POST with Post
			sed -e ' s/DELETE/Delete/g' #replace DELETE with Delete
	else
		echo "$LINE"
	fi #>> src/Generated/intermediate-definition-params.txt
done 

