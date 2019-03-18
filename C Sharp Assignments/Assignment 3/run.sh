

rm trafficlightframe.dll
rm trafficlight.exe
ls -l

mcs -target:library -r:System.Windows.Forms.dll -r:System.Drawing.dll -out:trafficlightframe.dll trafficlightframe.cs

mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:trafficlightframe.dll -out:trafficlight.exe Assignment3Main.cs

ls -l

./trafficlight.exe
