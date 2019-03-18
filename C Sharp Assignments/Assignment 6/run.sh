#!/bin/bash

#Author: Floyd Holliday

#Program name: Ricochet Ball

rm algorithm.dll

rm Ricochet-ui.dll

rm Rico.exe

mcs -target:library -r:System.Windows.Forms.dll -r:System.Drawing.dll algorithm.cs -out:algorithm.dll

mcs -target:library Ricochet-ui.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:algorithm.dll -out:Ricochet-ui.dll


mcs Ricochet_main.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:Ricochet-ui.dll -out:Rico.exe


./Rico.exe
