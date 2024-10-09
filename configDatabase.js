import mysql from 'mysql2/promise';

async function connectToDb() {
    try {
        const connection = await mysql.createConnection({
            host: 'localhost',
            user: 'root',
            password: 'Xuxu120521.',
            database: 'blog_database',
        });
        console.log('Connected to the database');
        return connection; // Đảm bảo trả về kết nối
    } catch (error) {
        console.log("Error connecting to the database: " + error);
        return null; // Trả về null nếu có lỗi
    }
}

export default connectToDb;
