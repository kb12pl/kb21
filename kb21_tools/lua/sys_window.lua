import('b12')
sql={}
--sql.ptr=MySqlite({})
win={ptr=B12_Integretion_Object, shortList={}, debug_tools={},short_number=0,event_lock=true}
win.is_debug=true
win.debug_tools['sys_query']='sys_query'


function win:reloadKb()
	kb=setmetatable({},{__index=function(t,k)  return win:loadScript(k) end})
end

win:reloadKb()

Ok=ok
function okt(a)
    ok(type(a))
end



function sf(tresc,...) 
    local par={...}
    local wyn=string.gsub(tresc, "([@#])(%^*)(%d+)", 	
    function(zna,nn,zmn)		  
      zmn=tonumber(zmn)      
      zmn=par[zmn] or ''
      if zna=='#' then
        zmn=string.gsub(zmn,"'","''")
        if zmn=='' and nn=='^' then
			return 'null'
		else
			return "'"..zmn.."'"
		end
      end
	  
		
      if zmn==''and nn=='^' then
		return 'null'
	  else
		return zmn  
	  end
    end
    )
    return wyn
end

function sr(...)
	return ...
end



function osf(...)
	local tmp=sf(...)
	ok(tmp)
	return tmp
end


function win:SetConfig(key,val)		
	win.ptr:SetConfig(tostring(key),tostring(val))
end

function win:stop()
	error('stop',0)
end

function win:error(comm)
	ok(comm, "error")
	error('stop',0)
end

function win:exit(a,...)          
    B12_Integretion_ret={a,...}        
    self:close()
end

function win:close()
    error('close',0)
end

function win:on_close()	
    self:close()
end

function win:exitFrame()
	self:cmd({frame_close=true})
end

function win:pcall(fun,x,...)    	
	local a,b=pcall(fun,x,...)			
	if not a then 		
		if b=='stop' then	
			self:stop()		
		elseif b=='close' then	
			self:close()		
		end
		ok(b)
	end	
end

function win:doString(name,script)     	
	a=self:doLoad(name,script)
	a()
end

function win:doLoad(name,script)   	
	if not script then	
		ok('script is null',name)
		self:stop()
	end
	local a,b=load(script,name)  
	if not a then			
		ok(b,name)
		self:stop()
	end
	return a
end

function win:loadScript(name)
	local script=self:readScript(name) 
	
	if not script or script=='' then	
		self:error((name or '') .. " : script is empty")
	end
	return self:doLoad(name,script)
end

function win:doScript(name)
	local script=self:readScript(name)    
	self:doString(name,script);
end

function win:readScript(name)
	if not name or name=='' then
		win:error("read script no name")
	end
		
    
    local file=io.open(win.ptr:GetConfig('prefix_file_script')..name..'.lua')
    if not file then    
       win:error("error open file or read: "..name)
    end
    
    local tmp=file:read('a')
    file:close()       
    if string.byte(tmp,1)==239 and string.byte(tmp,2)==187 and string.byte(tmp,3)==191 then
        tmp=string.char( string.byte(tmp,4,string.len(tmp)) )
    end
    return tmp;            
--[=[ 	return sql:scalar(string.format([[select script from kb_scripts where name='%s'	]],name))	 ]=]
end

function win:saveScript(name,code)       
    local file=io.open(win.ptr:GetConfig('prefix_file_script')..name..'.lua','w+')
    if not file then    
       win:error("error open file to wrire: "..name)
    end
    
    file:write(code)
    file:close()       
end



function win:set_layout(arg)
    ctrl.layout_dock:as_dock()
    if arg.nav=='top' then
        ctrl.nav:as_stack_h('layout_dock',{top=true})
    else
        ctrl.nav:as_stack_v('layout_dock',arg.nav)
    end
    if arg.grid then 
        ctrl.grid:as_grid('layout_dock')
        win:short('ctrl-enter',"ctrl.grid:focus()")     
    end
end
       


function win.onHelp()
    win:code({name="adminHelp"}) 
end


function xtab(tab)
	if type(tab)~='table' then
		ok('xtab varable not table')
		return
	end
	local com=''
	for k,v in pairs(tab) do		
		com=com .. k ..' : '.. tostring(v) ..'\n'
	end
	ok(com)
end


function win:global(key,val)        
	key=tostring(key)	
	if val then        
		--val=tostring(val)
		self.ptr:SetGlobal(key,val)
		return val
	else        
		return self.ptr:GetGlobal(key)
	end
end

function win:global2(key,val)        
	key=tostring(key)
	if val then        
		--val=tostring(val)
		self.ptr:SetGlobal2(key,val)
		return val
	else        
		return self.ptr:GetGlobal2(key)
	end
end


function win:send(smb,script)    
	return self:cmd({smb=smb,script=script})
end






function win:panel(arg)                        
	B12_Integretion_ret=nil;     	
	self.ptr:NewWindow(arg)     
	if B12_Integretion_ret then
		 return table.unpack(B12_Integretion_ret)
    end
end

function win:sql(query,ret_error)
      if not query or query=='' then
      		self:error("query is empty")
      	end	
	
	local ret=self.ptr:Sql({query=query})
	
	if ret.isError then
		if ret_error then		
			return nil,nil,ret.error
		else
			self:error(ret.error..'\n\n'..query)
		end	
	end
	local tab={}
	local cols=ret.cols
	local rows=ret.rows
	local tmp
	for i=0, rows-1 do
		tmp={}
		for j=0, cols-1 do
			table.insert(tmp,ret:Get(i,j))
		end
		table.insert(tab,tmp) 
	end
	local lab={}
	for j=0, cols-1 do
		table.insert(lab,ret:GetLabel(j))
	end
	return tab,lab;
end

function win:maximized()
	self.ptr:CmdWin({maximized=true})
end

function win:size(d,h)
	self:cmd({width=d,height=h})
end



function win:cmd(arg)
	if self.ptr:CmdWin(arg) then				
		if xtab then	
			xtab(arg)
		else	
			ok('error win:cmd')
		end			
		self:stop()
	end
end


function win:mess(a,b,t)	
	b=b or 'Komunikat'
	local arg={message=tostring(a),caption=tostring(b)}
      if true or t=='yes_no' then
      		arg.yes_no=true
      	end
      	self:cmd(arg)
       return arg.yes 
end


function xlog(...)
	ok(xlog_concat(...))
end



function xlog_concat(...)    
  local t={...}  
  local tmp=''
  for i=1,#t do    
    if type(t[i])=='boolean' then
      if t[i] then
        t[i]='true'
      else
        t[i]='false'    
      end    
    else 
      t[i]=tostring(t[i])
    end
    tmp= tmp.. ' ( ' ..(t[i] or '')..' ) '
  end
  if #t==0 then
    tmp='xl empty ->'..os.date("%H:%M:%S")
  end
  return tmp
end

function xlogclear()
	KbWindow.KbWindowOkClear()
	xlog(os.date())
end






function win:on_boot(reload)        
    if reload then
    	self:cmd({clear=true})
    end
    inarg=B12_Integretion_arg.arg or {}
    self.smb=B12_Integretion_arg.smb
    self.isPage=B12_Integretion_arg.page
    self.script=B12_Integretion_arg.script    
    
    
    ctrl=setmetatable({},{    __index=function(t,k)             
    t[k]=setmetatable({id=k,bind=0;cols={},key_id=0,keys={},ncols=0 },     {    __index=kb.sys_ctrl()             })         
    return t[k]     
    end})
    ctr=ctrl
    
    if not self.script or self.script=='' then
        win:error(" win:on_boot / no script")
    end
    
    kb[self.script]()
    
    if win.on_create then
    	win:on_create()
    end        
    
    self:short("F1",string.format("kb.sys_dialog('sys_code') "),true)    
    self:short("F2",string.format("kb.sys_dialog('sys_code',{name='%s'}) ",self.script),true)    
    self:short("F3",string.format("win:reloadKb()   kb['%s']()", self.script),true)    
    self:short('Ctrl+F3',"win:on_boot(true)")     
    self:short('Shift+Ctrl+F3',"win:on_boot(true)")     
    self:short("F5","if win.on_show then win:on_show() end",true)    
    self:short("F9","if win.on_test then win:on_test() end",true)    
    self:short("F11","kb.sys_dialog('sys_query')",true)    
    self:short("F12","win:on_tools()",true)    


     
	if self.isPage then
		self:short("Ctrl+W","win:on_close()" )
	else
		self:short("Esc","win:on_close()")
		--jesli  blad
		--self:short("Esc","  error('close',0)")
	end      

	if reload and self.on_load then
		self:on_load()
	end
    win.event_lock=false
end

win.shortChange={}
win.shortChange['ESC']='Escape'
win.shortChange['ENTER']='Enter'
win.shortChange['1']='D1'




 function win:short(key,fun,boot)
	key=string.upper(key)
	local shift=string.find(key,'SHIF') and true or false
	local ctrl=string.find(key,'CTRL') and true or false
	local alt=string.find(key,'ALT') and true or false
	local kod=string.match(key,'(%w+)$')
	
	kod=self.shortChange[kod] or kod 
    
    
    if boot and self.shortList[key] then
    	return
    end
    self.shortList[key]=fun
    self.ptr:Shortcut(key,kod,shift,alt,ctrl)    
end



function win:onShort(key)           
     self:doString('win:on_short - '..(key or "error short key"),self.shortList[key])         
end







function win:do_debug_tool(tool)
	kb.sys_dialog(tool)
end

function win:on_tools()
	local tab={}
	for k,v in pairs(ctrl) do
		if v.isButton then
			tab[v.id]=v.id
		end
	end
	
	if self.is_debug then
		for k,v in pairs(self.debug_tools) do
			tab[k]=v
		end
	end
	
	local tmp=kb.get_list('',tab)
	if not tmp then
		return 
	end
	
	if self.debug_tools[tmp] then
		return self:do_debug_tool(tmp)		
	end
	
	
	if tmp and ctrl[tmp].event then			   
	       ctrl[tmp]:focus()
		ctrl[tmp]:event()
	end
end

function B12_Integretion_Function(event,id,par_1,par_2)    
	--xlog(event,id,par_1,par_2)
	if win.event_lock then
		return
	end	
    if id=="" then  		
        if win[event] then     
	        win:pcall(win[event],win,par_1,par_2)                            
        end        
        return
    else    
        
        local control=ctrl[id]
        if not control then	
            return
        end          
        
        local fun,key,arg
        
        if control.isGrid then        
            fun=control[control.cols[par_1]]            
            key=tonumber(par_2) and control.keys[tonumber(par_2)]        
        elseif control.is_text then                        
            key=par_1            
            fun=control.event           
        else
            fun=control.event           
        end
        
        if fun then             
            win:pcall(fun,control,key)    
        end            
    end
end