function win:on_create()			win:size(500,500)	ctrl.dock:as_dock()		ctrl.filter:as_text('dock','',{enter=true,top=true,key=true})		ctrl.list:as_list('dock','',{enter=true,width=200,height=300})			win:short("F1","")
	win:short("F2","")	win:short("F12","")end
function win.on_load()				
	ctrl.filter:focus()	
	win.onShow()	
end

function win.onShow()
	ctrl.list:onShow()	
end

function ctrl.filter:event(key)  		

	if key=='Return' then	
		doExit(ctrl.list:getKey())		elseif key=='Up' then	
		ctrl.list:select(-1)
	elseif key=='Down' then			
		ctrl.list:select(1)
	elseif key=='PageUp' then			
		ctrl.list:select(-5)
	elseif key=='Next' then					
		ctrl.list:select(5)
	elseif key=='Left' or key=='Right' then		
		return
	else		
		win.onShow()
	end
end
function doExit(key)
	win:exit(inarg.data[key],key)
end

function ctrl.list:onShow()
 	self:clear()
 	local filter=ctrl.filter:get()
 	filter=string.lower(filter)      
       
	for k,v in pairs(inarg.data) do	     
	      if filter=='' or string.match(string.lower(v),filter) then	      	
			self:add(v,k)										
		end		
	end
	
	ctrl.list:select()
end

function ctrl.list:event()
	doExit(ctrl.list:getKey())	
end
