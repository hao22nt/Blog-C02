import connectToDb from "../../configDatabase.js"; // Connect to database
const db = await connectToDb();

class BlogRepository {
  constructor() {
    this.db = db;
  }

  async getBlogs() {
    const [result] = await this.db.query("SELECT * FROM blogs");
    return result;
  }

  async getBlogById(id) {
    const [result] = await this.db.query("SELECT * FROM blogs WHERE id =?", [
      id,
    ]);
    return result[0];
  }

  async saveContact(contact) {
    const { title, author, message } = contact;
    const sql =
      "INSERT INTO contacts (title, author, message) VALUES (?, ?, ?)";

    await this.db.query(sql, [title, author, message]);
  }
}

export default new BlogRepository();