a=kb.get_list_query('',"select name,name from kb_scripts")



--[=[
a=kb.sql_list("select name from kb_scripts order by name")
a=kb.get_list('',a)
ok(a)
]=]