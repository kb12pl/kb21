win:SetConfig("console_loop_time",240000)
curarg={user='console'}
xlogd=ok

fgt={}

ok(123);

function fgt.gt_wersja_systemu()
    return 0
end

function fgt.SystemGetSkrypt(script)  
  local tab=win:sql("select zdarzenie_tr from bbzdarzenia where zdarzenie_sb='" .. script .. "'")      
  return tab[1][1]
end

setmetatable(fgt,
{ 
__index=function(t,k)     
  local a,b
  a,b=load(t.SystemGetSkrypt(k) ,k)  
  if not a then
    xlogd(b)
    error('sys_stop',0) 
  end
  rawset(t,k,a)
  return a
end
})
gt2=fgt

function fgt.sqlQuery(query)      
   local tab=win:sql(query)    
   if tab[1] then
    return tab[1][1]
   end 
end

function fgt.sqlValues(query)
    local tab=win:sql(query)    
    if tab[1] then
        return table.unpack(tab[1])
    end
end

function fgt.sqlTable(query)
    local tab=win:sql(query)    
    return tab
end

function fgt.sqlList(query)
    local tab=win:sql(query)    
    local list={}
    for k,v in pairs(tab) do
       table.insert(list,v[1]) 
    end
    return list
end

function gt2.SystemProtectedModeIsError(a,b,...)  
  if a then
    return b,...
  end
  if b=='sys_close' then
    error('sys_close',0) 
  end
  error('sys_stop',0)       
end

function gt2.SystemProtectedModeError(b)      
  if b=='sys_close' or b=='sys_stop' then
    return b
  end   
  xlogd(b)
  return 'sys_stop'
end

function gt2.SystemProtectedMode(f,...)         
  return gt2.SystemProtectedModeIsError(xpcall(f,gt2.SystemProtectedModeError,...))
end

function sys_x_position(x)        
  local p=''
  for k=(x or 2),10 do
    local t=debug.getinfo(k)        
    if t and t.name then                              
      p=' -> '..(t.name or '')..'() '..t.currentline..''..p
    else      
      break
    end    
    k=k+1
  end     
  return p
end
function xlogd(...)
    xlog('\n"xlogd" '..sys_x_position(3))
    xlog(...)      
end


fgt.SystemProtectedMode(fgt.SystemZdarzenieFunkcjaFrameTimeServer,true)

