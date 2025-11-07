-- Conecte-se como postgres primeiro
-- psql -U postgres

-- Cria o usuário
CREATE USER estudo_user WITH PASSWORD 'Estudo123!';

-- Cria o banco de dados
CREATE DATABASE rotina_estudos 
    WITH 
    OWNER = estudo_user
    CONNECTION LIMIT = -1;

-- Conecte ao banco rotina_estudos
-- \c rotina_estudos

-- Concede privilégios
GRANT ALL PRIVILEGES ON DATABASE rotina_estudos TO estudo_user;

-- Cria a tabela de tarefas de estudo
CREATE TABLE tarefas_estudo (
    id SERIAL PRIMARY KEY,
    materia VARCHAR(100) NOT NULL,
    tema VARCHAR(200) NOT NULL,
    tempo_estudo_minutos INTEGER NOT NULL,
    prioridade VARCHAR(20) NOT NULL CHECK (prioridade IN ('Baixa', 'Média', 'Alta')),
    data_criacao TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    concluida BOOLEAN DEFAULT FALSE,
    data_conclusao TIMESTAMP NULL
);

-- Concede permissões na tabela
GRANT ALL PRIVILEGES ON TABLE tarefas_estudo TO estudo_user;
GRANT ALL PRIVILEGES ON SEQUENCE tarefas_estudo_id_seq TO estudo_user;