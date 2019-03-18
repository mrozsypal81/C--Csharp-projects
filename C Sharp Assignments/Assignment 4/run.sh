
rm assignment4frame.dll
rm movingball.exe
ls -l

mcs -target:library -r:System.Windows.Forms.dll -r:System.Drawing.dll -out:assignment4frame.dll assignment4frame.cs

mcs -r:System.Windows.Forms.dll -r:System.Drawing.dll -r:assignment4frame.dll -out:movingball.exe assignment4main.cs

ls -l

./movingball.exe
