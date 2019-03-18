
rm flashinglinealg.dll
rm flashinglineframe.dll
rm flashingline.exe
ls -l
mcs -target:library -r:System.Windows.Forms.dll -r:System.Drawing.dll -out:flashinglinealg.dll flashinglinealg.cs

mcs -target:library -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:flashinglinealg.dll -out:flashinglineframe.dll flashinglineframe.cs

mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:flashinglineframe.dll -out:flashingline.exe flashinglinemain.cs

ls -l

./flashingline.exe
