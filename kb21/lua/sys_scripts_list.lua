local this=...
list=this.list
this:add('indeksy_a',true,false,'Ind-A', true,'Indeksy')
this:addT('indeksy_b',{smb='Ind-B', link='Ind-A', title='Wybierz Indeks'})
this:addT('indeksy_wybierz')
this:add('indeksy_w_dodaj',true,false,'indeksy-dodaj')
list.tech_a={window='Tech-A', title='Technologie',page=true}
list.tech_e={window='Tech-E1', title='Edycja Technologii',work=true}

this:addT('web_lua',{smb='Web-Lua',page=true,work=true})
this:addT('web_google',{smb='Web-Google',page=true,work=true})

this:add('get_list')
this:add('get_text_empty_stop',true,true)
this:add('get_text',true,true)
list.sql={work=true}
list.sql_tab={work=true}
list.sql_tabn={work=true}
this:add('sql_create_database',true,true)
this:add('sys_boot',true,true)
this:add('sys_code',true,true,'sys_code',true)
list.sys_console={}
this:add('sys_ctrl',true,true)
this:add('sys_dialog',true,true)
this:add('sys_get',true,true,'sys_get')
this:add('sys_global',true,true)
list['sys_help']={work=true}
list['sys_list']={window='sys_list'}
list['sys_log']={work=true}
this:add('sys_page',true,true)
this:add('sys_query',true,true,'sys_query',true)
list.sys_scripts={work=true}
list.sys_scripts_list={work=true}
this:add('sys_testy',true,true,'ala',true)
this:add('sys_window',true,true)