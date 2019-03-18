#!/bin/bash

#Author: Floyd Holliday
#Mail:  holliday@fullerton.edu

echo "Program: Sinewave Trace"
echo "Draw a sine curve in real time"

echo "Compile Sinecurve.cs"
mcs -target:library Sinecurve.cs -out:Sinecurve.dll

echo "Compile Sineframe.cs"
mcs -target:library Sineframe.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:Sinecurve.dll -out:Sineframe.dll

echo "Compile Sinewave.cs"
mcs -target:exe Sinewave.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:Sineframe.dll -out:Sine.exe

echo "Run the program"
./Sine.exe

echo "The bash script will finish now"


