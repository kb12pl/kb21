local this={list={}}
function this:add(name, work, func, window, page,title)
	self.list[name]={work=work, is_func=func, window=window, page=page,title=title}
end
function this:addT(name,tab)
       tab=tab or {}       
	tab.window=tab.smb or tab.window	
	self.list[name]=tab
end

function this:work_list()
	local tmp={}
	for k,v in pairs(self.list) do
		if v.work then
			table.insert(tmp,k)		
		end
	end
	table.sort(tmp)
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
			tmp[k]=v.window..' - '..(v.title or'')
		end
	end

	local a,b=kb.get_list('',tmp,true)	
	if a then	
		self:run_uni({page=true},b)
	end
end
function this:find_script(window)	for k,v in pairs(self.list) do		if v.window==window then			return k		end	endend

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
       	ok('brak skryptu dla: ' .. (script or window or ' ?? '))
       	return
       end        
       local script_to_run=script
       if a.link then
       	script_to_run=self:find_script(a.link)
	       if not script_to_run then
	       	ok('brak skryptu dla linku: ',a.link)
	       	return
	       end      	       
       end
	
	par.smb=a.window
	par.script=script_to_run
	par.title=a.title	
	par.arg=arg		
	return win:panel(par)
end
kb.sys_scripts_list(this)

return this