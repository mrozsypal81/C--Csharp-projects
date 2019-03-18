

rm Drawshapesframe.dll
rm Drawshapes.exe
ls -l

mcs -target:library -r:System.Windows.Forms.dll -r:System.Drawing.dll -out:Drawshapesframe.dll Drawshapesframe.cs

mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:Drawshapesframe.dll -out:Drawshapes.exe Assignment1Main.cs

ls -l

./Drawshapes.exe
