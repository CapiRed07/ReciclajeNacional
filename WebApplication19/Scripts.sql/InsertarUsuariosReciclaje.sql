-- 1. Llenar Usuarios (Aparecerán con idusuario 1 y 2)
INSERT INTO dbo.usuario (nombre, correo, provincia, puntos, pwd, rol)
VALUES 
('Juan Pérez', 'juan@email.com', 'None', 150, '$2a$10$h7FqPinh1LN5Dvi8VTp2YuNO2BBGPhyMR5dd0rauIEcBQUlAwO/Vu', 'user'),
('María López', 'maria@email.com', 'San José', 30, '$2a$10$h7FqPinh1LN5Dvi8VTp2YuNO2BBGPhyMR5dd0rauIEcBQUlAwO/Vu', 'admin');