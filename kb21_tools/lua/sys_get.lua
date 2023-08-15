function win:on_create()
 	win:size(500,200)
	ctrl.stack:as_stack_v()
	ctrl.text:as_text('stack','',{enter=true})
	ctrl.stack_ok:as_stack_h('stack','',{center=true})
	ctrl.ok:as_button('stack_ok','Ok',{})
	ctrl.cancel:as_button('stack_ok','Cancel',{})		
end

function win:on_load()
	ctrl.text:set(inarg.def)
	ctrl.text:focus()
end
function ctrl.text:event()
	ctr.ok:event()
end

function ctrl.ok:event()
	win:exit(ctrl.text:get())
end