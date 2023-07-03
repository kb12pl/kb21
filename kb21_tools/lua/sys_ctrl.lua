local ctrl_meta={idSerial=1,ptr=win.ptr,}

function ctrl_meta:newLabel(master) 
	self.ptr:NewCtrl({parent=master.parent,label=master.label, isLabel=true})
end

function ctrl_meta:newCtrl(parent,label,arg)
	arg.id=self.id
	arg.parent=parent		
	self.parent=parent
	
    if label then
		local a,b,c=string.match(label,'^(%-?)([%+%.]?)(.*)')            
		if c=='' then
				c=self.id
		end
	    
		if a=='-'  then
			win.short_number=win.short_number+1       
			win:short('Ctrl+'..win.short_number, sf('ctrl[#1]:focus() if ctrl[#1].onEvent and ctrl[#1].isButton then ctrl[#1].onEvent() end',self.id)   )       
		end
	    
		self.label =c
		arg.label=c
	end
	if self.is_button then
		self.label=self.id
	end
      
      
	if self.is_search then
	    ctrl[arg.id..'_inner_label']:newLabel(self)
	end
	
	
	if arg.left then
		arg.HorizontalAlignment=0
		arg.Dock=0
	elseif arg.center then
		arg.HorizontalAlignment=1
		arg.VerticalAlignment=1
	elseif arg.right then
		arg.HorizontalAlignment=2
	elseif arg.top then
		arg.VerticalAlignment=0
		arg.Dock=1
	elseif arg.buttom then
		arg.VerticalAlignment=2
		arg.Dock=3
	elseif arg.stretch then	
		arg.VerticalAlignment=3
		arg.HorizontalAlignment=3
	end
	
	--xtab(arg)	
	self.ptr:NewCtrl(arg)
	return self
end

function ctrl_meta:as_web(parent,label,arg)
	arg=arg or {}
	arg.isWeb=true
	self.isWeb=true
	return self:newCtrl(parent,label,arg)
end

function ctrl_meta:as_code(parent,label,arg)
	arg=arg or {}
	arg.isCode=true
	self.isCode=true
	return self:newCtrl(parent,label,arg)
end

function ctrl_meta:as_stack_v(parent,label,arg)
	arg=arg or {}	
	arg.isStack=true
	self.isStack=true
	return self:newCtrl(parent,label,arg)
end

function ctrl_meta:as_stack_h(parent,label,arg)
	arg=arg or {}
	arg.horizontal=true
	arg.isStack=true
	self.isStack=true
	return self:newCtrl(parent,label,arg) 
end

function ctrl_meta:as_dock(parent,label,arg)
	arg=arg or {}
	arg.isDock=true
	self.isDock=true
	return self:newCtrl(parent,label,arg)
end

function ctrl_meta:as_button(parent,label,arg)
	arg=arg or {}	
	arg.isButton=true
	self.isButton=true	
	if not label or label=='' then
		label=self.id
	end		
	return self:newCtrl(parent,label, arg)
end

function ctrl_meta:as_label(parent,label,arg)
	arg=arg or {}	
	arg.isLabel=true
	self.is_label=true	
	if not label or label=='' then
		label=self.id
	end		
	return self:newCtrl(parent,label, arg)
end

function ctrl_meta:as_search(parent,label,arg)
	arg=arg or {}
	if not arg.width then
		arg.width=100
	end
	arg.onEnter=true	
	arg.isText=true
	self.is_search=true
	return self:newCtrl(parent,label,arg)	
end

function ctrl_meta:as_text(parent,label,arg)
	arg=arg or {}
	if not arg.width then
		arg.width=100
	end
	arg.onEnter=arg.enter	
	arg.onKey=arg.key
	arg.isText=true
	self.is_text=true	
	return self:newCtrl(parent,label,arg)
end

function ctrl_meta:as_grid(parent,label,arg)
	arg=arg or {}
	arg.isGrid=true
	self.isGrid=true
	return self:newCtrl(parent,label,arg)
end

function ctrl_meta:as_list(parent,label,arg)
	arg=arg or {}
	arg.isList=true
	self.isList=true
	return self:newCtrl(parent,label,arg)
end


function ctrl_meta:cmd(arg)
	arg.id=self.id
	if self.ptr:CmdCtrl(arg) then				
		if xtab then	
			xtab(arg)
		else	
			ok('error ctrl:cmd')
		end			
		win:stop()
	end
end

function ctrl_meta:focus()	
    local tmp=(self.isGrid and 'focus_datagrid') or 'focus'
	self:cmd({[tmp]=true})		
end



function ctrl_meta:get()	
	local arg={get=true}
	self:cmd(arg)
	return arg.text
end

function ctrl_meta:getCurrentWorld()	
	local arg={current_word=true}
	self:cmd(arg)
	return arg.text
end

function ctrl_meta:getCurrent()
	local arg={current_word=true}
	self:cmd(arg)
	return arg.text
end	


function ctrl_meta:getKey()	
	local arg={getKey=true}
	self:cmd(arg)	
	local key=tonumber(arg.key)
	return self.keys[key]
end


function ctrl_meta:add(val,key)
       if key then
       	table.insert(self.keys,key)
       	key=#self.keys
       end       
	self:cmd({add=val,key=key})
end

function ctrl_meta:select(idx)
	idx=idx or 0	
	self:cmd({select=idx})
end

function ctrl_meta:set(a,b,c)
   if self.isGrid then 
	return self:cmd({row=a,col=b,set=c or ''})
   end
  
   self:cmd({set=a or ''})
end

function ctrl_meta:clear()
	self:cmd({clear=true})
end

function ctrl_meta:setKey(row,key)    
    self.key_id=self.key_id+1
    self.keys[self.key_id]=key	
    self:cmd({key=self.key_id,row=row})
end

function ctrl_meta:get_caret()
	arg={caret=true}
	self:cmd(arg)
	return arg.caret
end

function ctrl_meta:set_caret(val)
	arg={caret_set=val}
	self:cmd(arg)	
end



function ctrl_meta:sql(query)
    self:clear()
    local tab,names=kb.sql_tab(query)
	if not tab then	
		return
	end
	local key	
	for k,v in pairs(tab) do
	  key={}
	  for m=1, math.min(self.ncols,#names) do 
		self:set(k,m,v[m])					
		key[names[m]]=v[m]
	 end
	self:setKey(k,key)
	end
end



function ctrl_meta:add_column(name,key)
       key=key or name
       self.ncols=self.ncols+1
	self.cols[name]=key
	self.bind=self.bind+1
	self:cmd({column=name,bind=self.bind})	
end




function ctrl_meta:find(what,back) 	
	self.find_last_what=what
	local code=self:get()    		
	local car=self:get_caret()
	local a=string.find(code ,what, car+1+string.len(what))  
	if not a and car>0 then
		a=string.find(code ,what,1)  
	end	
	if a then	
		self:set_caret(a-1)
	else
		ok('Not Found')
	end
end

function ctrl_meta:find_ask()
	local a=kb.get_text_empty_stop('Find')	
	self:find(a)
end

function ctrl_meta:find_next()
	if self.find_last_what then
		self:find(self.find_last_what)
	else
		self:find_ask()
	end
end

return ctrl_meta