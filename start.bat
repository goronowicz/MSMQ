SET receiver=MSMQ.MulticastMessageReceiver\bin\Debug\MSMQ.MulitcastMessageReceiver.exe
SET room1=.\Private$\room1
SET room2=.\room2
SET room3=.\room3
SET ipAddres=224.0.1.1:8081

ECHO %receiver%

start %receiver% %room1% %ipAddres%
start %receiver% %room2% %ipAddres%
start %receiver% %room3% %ipAddres%
MSMQ.MulticastMessageSender\bin\Debug\MSMQ.MulticastMessageSender.exe %room1%;%room2%;%room3% %ipAddres%

rem start %receiver% %room1% 

rem MSMQ.MulticastMessageSender\bin\Debug\MSMQ.MulticastMessageSender.exe %room1%

