#!/bin/bash
script_path=$(dirname $(realpath -s $0))/../

function add_license () {
    (find "$script_path" -name $1 | grep -v "/bin/" | grep -v "/obj/" )|while read fname; do
        line=$(sed -n '2p;3q' "$fname")
        if ! [[ "$line" == " * Licensed to Elasticsearch B.V. under one or more contributor" ]] ; then
            cat "${script_path}.github/license-header.txt" "$fname" > "${fname}.new"
            mv "${fname}.new" "$fname"
        fi
    done
}

add_license "*.cs"