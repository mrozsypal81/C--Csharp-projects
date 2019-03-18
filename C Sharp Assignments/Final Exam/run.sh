#!/bin/bash

#Author: Floyd Holliday

#Program name: Ricochet Ball


rm catmouseui.dll

rm catmouse.exe

mcs -target:library catmouseui.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll -out:catmouseui.dll


mcs catmouse_main.cs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:catmouseui.dll -out:catmouse.exe


./catmouse.exe
