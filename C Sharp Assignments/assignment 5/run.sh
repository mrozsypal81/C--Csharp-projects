#!/bin/bash
#In the official documentation the line above always has to be the first line of any script file.  But, students have 
#told me that script files work correctly without that first line.

#Author: Jeremy Appleton
#Course: CPSC223n
#Semester: Spring 2017
#Assignment: 5
#Due: March 30, 2017.

#This is a bash shell script to be used for compiling, linking, and executing the C sharp files of this assignment.
#Execute this file by navigating the terminal window to the folder where this file resides, and then enter the command: ./build.sh

#System requirements: 
#  A Linux system with BASH shell (in a terminal window).
#  The mono compiler must be installed.  If not installed run the command "sudo apt-get install mono-complete" without quotes.
#  The three source files and this script file must be in the same folder.
#  This file, build.sh, must have execute permission.  Go to the properties window of build.sh and put a check in the 
#  permission to execute box.

echo First remove old binary files, if any exist
rm *.dll
rm *.exe

echo View the list of source files
ls -l

echo Compile Archspiral.cs to create the file: Archspiral.dll
mcs -target:library Archspiral.cs -out:Archspiral.dll

echo Compile Archframe.cs to create the file: Archframe.dll
mcs -target:library Archframe.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:Archspiral.dll -out:Archframe.dll

echo Compile Archmain.cs and link the two previously created dll files to create an executable file. 
mcs -target:exe Archmain.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:Archframe.dll -out:Arch.exe

#echo View the list of files in the current folder
#ls -l

echo Run the Assignment 5 program.
./Arch.exe

echo The script has terminated.












