local title,query=...

local tab=kb.sql_tab(query)
local data={}

for k,v in pairs(tab) do
    data[v[2]]=v[1]
end

return kb.get_list(title,data)


