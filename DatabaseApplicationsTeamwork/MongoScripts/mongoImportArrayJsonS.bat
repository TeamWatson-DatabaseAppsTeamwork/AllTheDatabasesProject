for /f %%f in ('dir /b w:\Json-Reports') do mongoimport -d SoftUni -c reports --jsonArray --file "w:\Json-Reports\%%f"
exit
PAUSE