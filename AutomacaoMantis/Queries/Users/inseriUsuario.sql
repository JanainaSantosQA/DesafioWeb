INSERT INTO mantis_user_table (
username, realname, email, password, enabled, protected, access_level, login_count, lost_password_request_count, failed_login_count, cookie_string, last_visit, date_created) 
VALUES (
'$username', '$realname', '$email', 'janaina@santos', '$enabled', 0, 25, 0, 0, 0, '$cookie', 1, 1);

SELECT username Username, realname RealName, email Email, id UserId, access_level AccessLevel
FROM bugtracker.mantis_user_table
WHERE ID = (SELECT MAX(id) FROM mantis_user_table);