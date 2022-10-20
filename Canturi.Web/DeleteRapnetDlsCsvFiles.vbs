
Dim strSites

'strSites="http://localhost:50322/Common/generateweeklystatusreportdata"'
strSites="http://103.8.127.150/Canturi/common/DeleteOldCsv"

Set http = CreateObject("Microsoft.XmlHttp")
http.open "GET", strSites, FALSE
http.send ""

set WshShell = nothing
set http = nothing