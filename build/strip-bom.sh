#!/bin/bash

script_path=$(dirname $(realpath -s $0))/../

(find "$script_path" -name "*.fs" | grep -v "/bin/" | grep -v "/obj/")|while read fname; do
    sed -i 's/\xEF\xBB\xBF//' "$fname"
done

(find "$script_path" -name "*.cs" | grep -v "/bin/" | grep -v "/obj/")|while read fname; do
    sed -i 's/\xEF\xBB\xBF//' "$fname"
done
