local query=...
local tab,lab=win:sql(query)
local ret={}
local row
for k,v in pairs(tab) do
row={}
for a,b in pairs(v) do
row[lab[a]]=b
end
ret[k]=row
end

return ret