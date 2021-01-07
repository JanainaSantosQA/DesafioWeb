INSERT INTO mantis_category_table (project_id, user_id, name, status)
VALUES(0,0,'$categoryName',0);

SELECT Id CategoryId, project_id ProjectId, name CategoryName
FROM mantis_category_table 
WHERE ID = (SELECT MAX(id) FROM mantis_category_table);