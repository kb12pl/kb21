fgt={}
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
xlogd=ok

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


fgt.SystemProtectedMode(fgt.SystemZdarzenieFunkcjaFrameTimeServer,true)

