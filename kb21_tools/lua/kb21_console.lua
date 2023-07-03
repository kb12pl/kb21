local tab,lab=win:sql("select 123 as aa ")

for k,v in pairs(tab) do    
    xlog(v)
end

    