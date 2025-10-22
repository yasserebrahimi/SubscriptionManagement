-- Seed minimal data for local compose
CREATE TABLE IF NOT EXISTS "__seed_marker" (id int primary key);
INSERT INTO "__seed_marker"(id) VALUES (1) ON CONFLICT DO NOTHING;
