local data=kb.sql_tab(...)
if not data then
    return
end

local ret={}

for k,v in pairs(data) do
    ret[v[1]]=v[2]
end

return ret;