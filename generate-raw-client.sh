#!/bin/bash

# This scripts scans the elasticsearch source code for all the registered REST endpoints 
# It will put the formatted output in $DEFINITIONOUTPUTFILE 
# [MethodName] [HttpVerb] [Route]

ESFOLDER="../elasticsearch"
DEFINITIONOUTPUTFILE="src/Generated/rest-actions.txt"
ESCOMMIT=$(cd $ESFOLDER | git rev-parse HEAD)

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

to_interface() {
	cat src/Generated/rest-actions.txt | 
	sed -e 's/\([^ ]*\) \([^ ]*\) \(.*\)$/\/\/\/<summary>\2 \3<\/summary>\nConnectionStatus \1\2(\3);\n\/\/\/<summary>\2 \3<\/summary>\nConnectionStatus \1\2Async(\3);/' |
		sed -e '2~2 s/\/{/, string /g' | #replace /{ with string param
	 	sed -e '2~2 s/}\([^,]*\)//g' | #remove } up to first , or )
		sed -e '2~2 s/(, string/(string/'|  #touch up first param
	    sed -e '2~2 s/\/[^,]*//g' | #remove urls bit from params.
	    #Render body parameter for all the verbs except GET and HEAD
	    sed -e '2~2 s/\(.*\)\(POST\|PUT\|DELETE\)\(.*\)$/\1\2\3, object body, NameValueCollection queryString = null);/' |
		
		# GET's and HEAD cannot have a BODY (.NET limitation)
	    sed -e '2~2 s/\(.*\)\(GET\|HEAD\)\(.*\)$/\1\2\3, NameValueCollection queryString = null);/' |

		sed -e '2~2 s/(, /(/g' | #remove any empty first params

		#Touching up the methodnames as best as we can
		sed -e '2~2 s/\(Get\w*\)GET/\1/g' | #remove starting Get if GET is already in the method name
		sed -e '2~2 s/\(Delete\w*\)DELETE/\1/g' | #remove DELETE if Delete is already in the method name (flows better)
		sed -e '2~2 s/\(Head\w*\)HEAD/\1/g' | #remove HEAD if Head is already in the method name (flows better)
		sed -e '2~2 s/\(Put\w*\)PUT/\1/g' | #remove PUT if Put is already in the method name (flows better)
		sed -e '2~2 s/GET/Get/g' | #replace GET with Get
		sed -e '2~2 s/HEAD/Head/g' | #replace HEAD with Head
		sed -e '2~2 s/PUT/Put/g' | #replace PUT with Put
		sed -e '2~2 s/POST/Post/g' | #replace POST with Post
		sed -e '2~2 s/DELETE/Delete/g' | #replace DELETE with Delete
		#return Task for async operations
		sed -e '2~2 s/ConnectionStatus\(.*\)Async/Task<ConnectionStatus>\1Async/g' | 
		cat
}

###
# - IRawElasticClient.cs
###
cat >src/NEST/IRawElasticClient.cs <<EOL
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///Generated using generate-raw-client.sh in NEST's root
namespace Nest
{
	///<summary>
	///Raw operations with elasticsearch
	///<pre>
	///This file is automatically generated from the elasticsearch repository itself
	///</pre>
	///<pre>
	///Generated of commit $ESCOMMIT
	///</pre>
	///</summary>
	public interface IRawElasticClient
	{
EOL

to_interface |
	sed -e 's/^\/\//\n\/\//' | #newline before xmldocs
	sed -e 's/^\(.\)/\t\t\1/' | #lines with data indent twice
	cat >> src/NEST/IRawElasticClient.cs


cat >>src/NEST/IRawElasticClient.cs <<EOL
	}
}
EOL

###
# - RawElasticClient-Client.cs
###

to_interface >src/Generated/tmp.txt

VERB=""
URL=""
PARAMS=""

cat >src/Nest/RawElasticClient-Generated.cs <<EOL
using System.Collections.Specialized;
using System.Threading.Tasks;

///Generated using generate-raw-client.sh in NEST's root
namespace Nest
{
	///<summary>
	///Raw operations with elasticsearch
	///<pre>
	///This file is automatically generated from the elasticsearch repository itself
	///</pre>
	///<pre>
	///Generated of commit $ESCOMMIT
	///</pre>
	///</summary>
	public partial class RawElasticClient : IRawElasticClient
	{
EOL

#TODO rewrite this to single sed chain like we do for IRawElasticClient
#Looping with while read line is wayyy to slow

cat src/Generated/tmp.txt | while read LINE ; do
	N=$((N+1))


	if [[ $LINE = *ConnectionStatus* ]];
	then
		# Check if the current method def is for an async method
		Async="";
		if [[ "$LINE" == *Async* ]]; then
			Async="Async";
			
		fi
		printf "\tpublic $LINE\n" |
				#remove trailing slash from line
				sed -e 's/;$//'

		
		printf "\t{\n"
		
		# If the route has no params we can just assign the route to path
		if [ -z "$PARAMS" ]; then
			printf "\t\tvar path = \"$URL\";\n"
		else 
			#render throwifnull checks for the route parameters
			printf "$PARAMS" |
				sed -e 's/\w* = \([^,]*\),*/\t\t\1.ThrowIfNullOrEmpty("\1");\n/g'
			#string format path
			printf "\t\tvar path = \"$URL\".Inject(new { $PARAMS });\n"
		fi
		
		

		# If the current verb is GET or HEAD we don't have a body to pass along
		if [ "$VERB" == "GET" ]; then
			printf "\t\treturn this.DoRequest$Async(\"$VERB\", path, queryString);\n"
		elif [ "$VERB" == "HEAD" ]; then
			printf "\t\treturn this.DoRequest$Async(\"$VERB\", path, queryString);\n"
		else 
			printf "\t\treturn this.DoRequest$Async(\"$VERB\", path, body, queryString);\n"
		fi

		printf "\t}\n"
	else
		#Get the verb from the comment line and remember it for the next line
		VERB=$(echo "$LINE" | sed -e 's/^.*<summary>\(.*\) .*$/\1/')

		# Get the route from the comment and remember it for the next line
		URL=$(echo "$LINE" | sed -e 's/^.*<summary>.* \(.*\)<\/summary>$/\1/')
		# This gets all the params in the URL and turns them into the body of an object initializer
		# i.e '/{x}/y/{z}' => 'x = x, y = y'
		PARAMS=$(echo $URL | 
			sed -e 's/[^{]*{\([^}]*\)}/\1 = \1, /g' | 
			sed -e 's/\/[^\/,]*//g' | sed -e 's/, $//')
		printf "\n\t$LINE\n"
		
	fi >> src/NEST/RawElasticClient-Generated.cs
done 

cat >>src/NEST/RawElasticClient-Generated.cs <<EOL
	}
}
EOL

rm -f src/Generated/tmp.txt