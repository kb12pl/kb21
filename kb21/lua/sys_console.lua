function win:on_create()
	ctrl.dock:as_dock()		
	ctrl.comm:as_text('dock','',{enter=true,buttom=true})	
	ctrl.cons:as_text('dock','',{})			
	
end


function win:on_load()
	ctrl.comm:focus()
end