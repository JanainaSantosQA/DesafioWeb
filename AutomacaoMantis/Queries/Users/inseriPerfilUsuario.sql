INSERT INTO mantis_user_profile_table (
platform, os, os_build, description) 
VALUES (
'$platform','$os', '$version', '$description');

SELECT *
FROM mantis_user_profile_table
WHERE ID = (SELECT MAX(id) FROM mantis_user_profile_table);