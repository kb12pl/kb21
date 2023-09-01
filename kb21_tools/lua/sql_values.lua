local tmp=kb.sql(...)
if tmp[1] then
	return table.unpack(tmp[1])
end