
rm TTTalgorithm.dll
rm TTTframe.dll
rm TTT.exe
ls -l
mcs -target:library -r:System.Windows.Forms.dll -r:System.Drawing.dll -out:TTTalgorithm.dll TTTalgorithm.cs

mcs -target:library -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:TTTalgorithm.dll -out:TTTframe.dll TTTframe.cs

mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:TTTframe.dll -out:TTT.exe Assignment2Main.cs

ls -l

./TTT.exe
