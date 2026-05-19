-- Run this script once in psql to set up the FlorenBooks database
-- Usage: psql -U postgres -f db_setup.sql

-- Create the database (run this part separately if needed)
-- CREATE DATABASE florenbooksdb;

-- Connect to the database before running the rest:
-- \c florenbooksdb

-- User role enum
DO $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'user_role') THEN
        CREATE TYPE user_role AS ENUM ('superAdmin', 'libraryAdmin', 'borrowAdmin', 'user');
    END IF;
END$$;

-- Users table
CREATE TABLE IF NOT EXISTS users (
    id          SERIAL PRIMARY KEY,
    email       VARCHAR(255) UNIQUE NOT NULL,
    password    VARCHAR(255) NOT NULL,
    role        user_role NOT NULL DEFAULT 'user',
    created_at  TIMESTAMP DEFAULT NOW()
);

-- Books table
CREATE TABLE IF NOT EXISTS books (
    id SERIAL PRIMARY KEY,
    titlu VARCHAR(255) NOT NULL,
    autor VARCHAR(255) NOT NULL,
    editura VARCHAR(255),
    anul INT,
    pret DECIMAL(10, 2),
    created_at TIMESTAMP DEFAULT NOW()
);
