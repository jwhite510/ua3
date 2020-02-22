#!/bin/bash

tac /mnt/c/Users/jonat/AppData/Local/Unity/Editor/Editor.log | while read -r line
do
	echo $line | sed -e 's/\\/\//g'
	if [[ $line == *"EndCompilerOutput"* ]];
	then
		break
	fi
	# if [[ $line == *"Refresh Completed time"* ]];
	# then
	# 	break
	# fi
done
