#!/bin/bash
script_path=$(dirname $(realpath -s $0))/../

function add_license () {
    (find "$script_path" -name $1 | grep -v "/bin/" | grep -v "/obj/" )|while read fname; do
        line=$(sed -n '1p;2q' "$fname")
        if ! [[ "$line" == "// Licensed to Elasticsearch B.V under one or more agreements." ]] ; then
            # awk joins the header with the existing file, inserting a newline between them
            awk '(NR>1 && FNR==1){print ""}1' "${script_path}.github/license-header.txt" "$fname" > "${fname}.new"
            mv "${fname}.new" "$fname"
        fi
    done
}

add_license "*.cs"