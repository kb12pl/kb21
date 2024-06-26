function win:on_create()
	--win:maximized()			
	ctrl.pp:as_dock('')	 
	ctrl.code:as_code('pp')	
	
	win:short('F1',"win:shortF1()")
	win:short('Ctrl-F1',"win:shortCtrlF1()")
	win:short('F2',"win:shortF2()")
	win:short('F3',"win:shortF3()")
	win:short('F4',"win:shortF4()")		
	win:short('Ctrl-F4',"win:shortF4()")		
	win:short('F5',"win:shortF5()")			
	win:short('Ctrl-F5',"win:shortCtrlF5()")		
	win:short('F6',"ctrl.code:find_next( ctrl.code:getCurrent() )")
	win:short('F7',"ctrl.code:find_ask()")
	win:short('F9',"win:shortF9()")
	win:short('F10',"win:shortF2('window')")
	win:short('F11',"win:shortF11()")
	win:short('F12',"win:shortF12()")	

end

function win:on_load(script)
	self:load_script( inarg.name or win:global('globalLastScript') )
end

function win:load_script(script)	
	self:save_last()	
	current_script=script
	win.ptr:CmdWin({title=script})
	ctrl.code:focus()
	ctrl.code:set()
	if script and script~='' then
		ctrl.code:set(win:readScript(script))		
		local tmp=win:global('globalLastScript_caret_'..script)
		if tmp then
			ctrl.code:set_caret(tmp)
		end		
	end	
end

function win:save_last()
	if current_script and current_script~='' then			
		self:global('globalLastScript3',self:global('globalLastScript2'))			
		self:global('globalLastScript2',self:global('globalLastScript1'))			
		self:global('globalLastScript1',self:global('globalLastScript'))			
		self:global('globalLastScript',current_script)			
		self:global('globalLastScript_caret_'..current_script,ctrl.code:get_caret())		
	end		
end

function win:on_close()			
	self:save_last()
	self:exit()
end

function win:shortF12()
	local tab={}
	tab.insert_func="Insert Fun".."ction"
	tab.query_tables="Tables and columns"
	local a,b=kb.get_list('',tab)
	if a then
		self[b](self)
	end
end

function win:insert_func()
	self:shortF1(true)
end
function win:query_tables()
	kb.sys_dialog('sys_tables')	
end

	
	
function win:shortF4()
	local tmp=ctrl.code:getCurrentWorld()		
	kb.sys_dialog('sys_code',{name=tmp})		
end
function win:shortF2(mode) 
  local tmp
  if  mode=="ctrl" then
  	tmp=win:readScript('sys_ctrl')
  elseif  mode=="window" then
  	tmp=win:readScript('sys_window')
  else
  	tmp=ctrl.code:get()    
  end
  local x={}  
  local y={}  
  local wzorzec='f'.."unction([^%(]*%([^%)]*%))"
  local l,k,t=string.find(tmp, wzorzec,k)
  while l do
    table.insert(x,t)
    y[t]=l+8   
    l,k,t=string.find(tmp, wzorzec,k)  
  end  
  table.sort(x)
  
  x=kb.get_list('',x)
  
  if not x then
  	return
  end
  
  if  mode=='control' then
  	ctrl.code:cmd({insert=string.sub(x,6) })
 elseif  mode=='window' then
  	ctrl.code:cmd({insert=x})
  else
  	ctrl.code:set_caret(y[x])  
  end
end

function win:shortF1(insert)	
	--local s=kb.get_list('',kb.sys_scripts():work_list())	--ok(s)
	local s=kb.get_list_query('',"select name,name from kb_scripts")
	
	if not s then
		return
	end	
	if insert then	
		ctrl.code:cmd({insert=s})
		return 
	end			
	win:load_script(s)
end


function win:shortCtrlF1(insert)
	ok(123)
end




function win:shortF3()
    self:sprawdz(ctrl.code:get())          
	win:saveScript(current_script,ctrl.code:get())
	win.ptr:CmdWin({title=current_script .. ' '.. os.date('%H-%M...%S')})	
end
function win:sprawdz(kod)
  local a,b
  a,b=load(kod,'','t',{})
  if not a then
    Ok(b)    
    return 
  end
  return true
end


function win:shortF9()
	self:shortF3()
	kb.sys_testy=nil
	kb.sys_testy()
end

function win:shortF11()
	kb.sys_dialog('sys_query')
end


function win:shortF5()
	self:shortF3()
	kb.sys_test()
end

function win:shortCtrlF5()

	local query=ctrl.code:get_selected()
	
	if query=='' then
		return
	end
	
	local tab,lab,err=win:sql(query,true)
	if err then
		ok(err..'\n\n'..query)
	end
end