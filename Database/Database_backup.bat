@echo off
REM  **************************************************************
REM  FileName: Database_backup.bat
REM  Purpose : Take requested database backup
REM  Auther:
REM  Date:
REM  Copyright (c) 2017 , www.glossyworks.org, All right reserved 

REM  **************************************************************


TITLE Database Backup Utility

SET BACKUP_PATH=E:\DataBases\Backup

SET HOSTNAME=PRINCE
SET USER_NAME=sa
SET PASSWORD=Test@12345
SET DATABASE_NAME=digiSMS

set FILE_NAME=%DATABASE_NAME%_%date:~0,2%_%date:~3,2%_%date:~6,8%-%time:~0,2%_%time:~3,2%_%time:~6,2%.bak

echo Please wait, Database backup is in progress
sqlcmd -S %HOSTNAME% -U %USER_NAME% -P %PASSWORD%  -Q "BACKUP DATABASE %DATABASE_NAME% TO DISK ='%BACKUP_PATH%\%FILE_NAME%'"

pause