local key,val=...
return win:global(key,
if val then
	win:setGlobal(key,val)
	return val
else
	return  win:global(key,val)
