win: SetConfig("secret_prefix","test")	
win: short('F1', "kb.sys_dialog('sys_code')")
win: short('F5', "kb.sys_test=nil kb.sys_test()")
win: short('F8', "kb.sys_scripts():run_list()")

win.event_lock=false