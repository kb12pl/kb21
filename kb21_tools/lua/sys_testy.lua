ok(123)
kb.get_list_query('',"select 123 ,1--name,name from kb_scrpts")






--[=[

--win.ptr:SetGlobalTable("a",kb.sys_scripts())
win.ptr:GetGlobalTable("a")
a=B12_Integretion_global
ok(type(a.list))
for k,v in pairs(a.list) do
	ok(k)
	return
end

]=]