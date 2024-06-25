local query=...

local tab=kb.sql_tab(query)

local tmp={}

for k,v in pairs(tab) do
	table.insert(tmp,v[1])
end
	


return tmp