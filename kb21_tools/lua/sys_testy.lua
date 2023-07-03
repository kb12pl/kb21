--win:cmd({serial_read=true})

--local symbol,arg=...

function win:http()	
	self.ptr:HttpClient({id=123,get=123})
	self.ptr:HttpClient({id=123,get=123,deepl=true})
end

function win:on_http(ret)
	ok(ret:Get("id"))	
	x=string.sub(ret:Get("a"),1,100)
	Ok("s"..x)

end

win:http()



