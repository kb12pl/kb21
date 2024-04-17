--win.ptr:SetGlobalTable("a",kb.sys_scripts())
win.ptr:GetGlobalTable("a")
a=B12_Integretion_global
ok(type(a.list))
for k,v in pairs(a.list) do
	ok(k)
	return
end