local this={}
function this:add(name, work, func, window, page,title)
	self.list[name]={work=work, is_func=func, window=window, page=page,title=title}
end

function this:create_list()
	self.list={}
	--   name, work, func, window, page,title   
	
	this:add('indeksy',true,false,'indeksy', true,'Indeksy')
	--this:add('indeksy_b',{smb='Ind-B', link='Ind-A', title='Wybierz Indeks'})
	--this:addT('indeksy_wybierz')
	--this:add('indeksy_w_dodaj',true,false,'indeksy-dodaj')
	self:add('tech_a', true, true, 'technologie', true, 'Technologie')
	--list.tech_e={window='Tech-E1', title='Edycja Technologii',work=true}
	
	self:add('get_list')
	self:add('get_list_query')
	self:add('get_text_empty_stop',true,true)
	self:add('get_text',true,true)
	
	self:add('kb21_test',true,true)		self:add('sql',true,true)
	self:add('sql_list',true,true)
	self:add('sql_tab',true,true)
	self:add('sql_tabn',true,true)
	
	self:add('system_export',true,true)
	
	self:add('sys_code',true,true,'sys_code',false)
	self:add('sys_console',true,true,'sys_console',false)
	self:add('sys_ctrl',true,true)
	self:add('sys_dialog',true,true)
	self:add('sys_get',true,true,'sys_get')
	self:add('sys_global',true,true)
	self:add('sys_help',true,true)
	self:add('sys_init_create_datebase',true)
	self:add('sys_init_win',true,true,'sys_init_win',true,'Sys init')
	self:add('sys_list',true,true,'sys_list')
	self:add('sys_log',true,true)
	self:add('sys_page',true,true)
	self:add('sys_query',true,true,'sys_query',true,'Sys query')
	self:add('sys_scripts',true,true)
	self:add('sys_scripts_win',true,true,'sys_scripts_win',true,'Sys scripts')
	self:add('sys_tables',true,true,'sys_tables',true,'Sys tables')
	self:add('sys_test',true,true)
	self:add('sys_window',true,true)
	
	
	self:add('web_lua',true,true,'web_lua',false,'web_lua')
	self:add('web_google',true,true,'web_google',false,'web_google')
	
	self:add('rcp_zdarzenia',true,true,'rcp_zdarzenia',true,'Zdarzenia')
	self:add('rcp_osoby',true,true,'rcp_osoby',true,'Osoby')
	self:add('rcp_osoby_upr',true,true,'rcp_osoby_upr',false,'Osoby uprawnienia')
	self:add('rcp_drzwi',true,true,'rcp_drzwi',true,'Drzwi')
	self:add('rcp_drzwi_upr',true,true,'rcp_drzwi_upr',false,'Drzwi uprawnienia')
	self:add('rcp_uprawnienia',true,true,'rcp_uprawnienia',true,'Uprawnienia')
	self:add('rcp_glowne',true,true,'rcp_glowne',true,'Wejście główne')
	self:add('rcp_hala',true,true,'rcp_hala',true,'Wejście hala')
	self:add('rcp_glowne_mies',true,true,'rcp_glowne_mies',true,'We-wy główne wg okresu')
	self:add('rcp_hala_mies',true,true,'rcp_hala_mies',true,'We-wy hala wg okresu')
end

function this:work_list()
	local tmp={}
	local last={}
	last[win:global('globalLastScript3')or '']=true
	last[win:global('globalLastScript2')or '']=true
	last[win:global('globalLastScript1')or '']=true
	last[win:global('globalLastScript')or '']=true
	last['']=nil
	for k,v in pairs(self.list) do
		if v.work and not last[k] then
			table.insert(tmp,k)		
		end
	end
	table.sort(tmp)
	for k,v in pairs(last) do		
		table.insert(tmp,1,k)		
	end
	return tmp
end


function this:show_code(name)
	local tmp=self.list[name or '']
	if tmp then
		kb.sys_dialog('sys_code',{name=name})	
	end
end

function this:run_list()
	local tmp={}	
	for k,v in pairs(self.list) do
		if v.page then
			tmp[k]=v.title or v.window
		end
	end
	local a,b=kb.get_list('',tmp,true)	
	if a then	
		self:run_uni({page=true},b)
	end
end

function this:find_script(window)
	for k,v in pairs(self.list) do
		if v.window==window then
			return k
		end
	end
end

function this:run(window,arg)
	return self:run_uni( {dialog=true} ,nil,window,arg)
end


function this:run_modeless(window,arg)      
	return self:run_uni({modeless=true},nil,window,arg)
end


function this:run_page(window,arg)
	self:run_uni( { page=true }, nil, window, arg)
end


function this:run_uni(par,script,window,arg)
	script=script or self:find_script(window)
 	local a=self.list[script]       

    if not a then
		xlog('run_uni','no script',script,'window',window,' ')
		return
    end        

    local script_to_run=script

    if a.link then
		script_to_run=self:find_script(a.link)
	    if not script_to_run then
			ok('run_uni brak skryptu dla linku: ',a.link)
	    return
	   end      	   
    end
	par.smb=a.window
	par.script=script_to_run
	par.title=a.title	
	par.arg=arg		
	return win:panel(par)
end


--this.list=win:global('kb_system_scripts_list')

this:create_list()	
return this