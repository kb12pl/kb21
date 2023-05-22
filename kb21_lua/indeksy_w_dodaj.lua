function win:on_create()
	win:size(500,200)
	ctrl.stack:as_stack_v()
	ctrl.but1:as_button('stack','',{left=true})
	ctrl.but2:as_button('stack','',{right=true})
end