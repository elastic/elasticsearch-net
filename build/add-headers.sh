#!/bin/bash

script_path=$(dirname $(realpath -s $0))/../


(find "$script_path" -name "*.fs" | grep -v "/bin/" | grep -v "/obj/")|while read fname; do
    line=$(head -n 1 "$fname")
    if [[ "$line" =~ ^/ ]] || [[ "$line" =~ ^# ]] ; then 
      echo "Skipped already starts with / or #: $fname"
    else 
      cat "${script_path}build/file-header.txt" "$fname" > "${fname}.new"
      mv "${fname}.new" "$fname"
    fi
done

(find "$script_path" -name "*.cs" | grep -v "/bin/" | grep -v "/obj/")|while read fname; do
    line=$(head -n 1 "$fname")
    if [[ "$line" =~ ^/ ]] || [[ "$line" =~ ^# ]] ; then 
      echo "Skipped already starts with / or #: $fname"
    else 
      cat "${script_path}build/file-header.txt" "$fname" > "${fname}.new"
      mv "${fname}.new" "$fname"
    fi
done
