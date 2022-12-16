local tmp=kb.get_text(...)

if not tmp or tmp=='' then
	win:stop()
end
return tmp