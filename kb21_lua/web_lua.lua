function win:on_create()
	ctrl.web:as_web('')
	ctrl.web:set("https://www.lua.org/manual/5.4/#index")
	win:short('F12',[[
	ctrl.web:cmd({jscript='window.alert("sometext");'})
	]])	
end