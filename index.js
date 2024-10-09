import express from "express";
import { engine } from "express-handlebars";
import path from "path";
import blogRepo from "./src/utils/blogRepository.js";
const app = express();

app.use(express.urlencoded({ extended: true }));

app.use(express.static(path.resolve("src/public")));

app.engine(
  "hbs",
  engine({
    extname: "hbs",
  })
);

app.set("view engine", "hbs");
app.set("views", "src/views");

app.get("/", async (req, res) => {
  const blogs = await blogRepo.getBlogs();
  console.log("blogs: ", blogs);

  res.render("home", {
    outStandingBlog: blogs[0],
    blogs: blogs.slice(1),
  });
});
app.get("/contact", (req, res) => {
  res.render("contact");
});

app.post("/contact", async (req, res) => {
  const { title, author, message } = req.body;

  // Validate dữ liệu
  if (!title || title.length < 5) {
    return res.send("Title is required and must be longer than 5 characters.");
  }
  if (!author || author.length < 3) {
    return res.send("Author is required and must be longer than 3 characters.");
  }

  // Lưu thông tin liên hệ vào cơ sở dữ liệu
  blogRepo.saveContact({ title, author, message });

  res.send("Contact information has been sent.");
});

app.get("/blog/:id", async (req, res) => {
  const blogId = req.params.id;

  try {
    const blog = await blogRepo.getBlogById(blogId);
    if (!blog) {
      return res.status(404).send("NOT FOUND");
    }
    res.render("blogDetail", { blog });
  } catch (error) {
    console.error(error);
    res.status(500).send("An error occurred while retrieving post data.");
  }
});

app.listen(3000, () => {
  console.log("Serever is running at http://localhost:3000/");
});