function win:on_create()
	ctrl.stack:as_stack_v()	
	ctrl.code:as_code('stack','',{height=400})	
	ctrl.rap:as_grid('stack','',{height=400})	
	for i=1,20 do
		ctrl.rap:add_column(i)
	end		
	win:short('F5',win.shortF5 )			
end

function win.on_load()	
	ctrl.code:set(win:global('globalLastQuery'))
	local tmp=win:global('globalLastQueryCaret')
	if tmp then
		ctrl.code:set_caret(tmp)
	end		
	
	ctrl.code:focus()		
end

function win:on_close()	
	win:global('globalLastQuery',ctrl.code:get())			       	
	win:global('globalLastQueryCaret',ctrl.code:get_caret())				
	win:exit()
end

function win:shortF5()

	local query=ctrl.code:get_selected()
	if query=='' then
		query=ctrl.code:get()
	end
	if query=='' then
		return
	end
	
	ctrl.rap:clear()

	local tab,lab,err=win:sql(query,true)

	if not tab then
		ctrl.rap:set(1,1,err)	
		return
	end

	

	for k,v in pairs(tab) do	

		for m,n in pairs(v) do			

			ctrl.rap:set(k,m,n)	

		end

	end

end