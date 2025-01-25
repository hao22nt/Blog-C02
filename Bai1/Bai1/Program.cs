namespace Bai1;
using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    static void Main()
    {
        // Ghi lại thời gian bắt đầu
        DateTime startTime = DateTime.Now;

        // 1. Random 10,000 số từ 1 đến 1,000,000
        Random random = new Random();
        List<int> list1 = new List<int>();
        for (int i = 0; i < 10000; i++)
        {
            list1.Add(random.Next(1, 1000001));
        }

        // 2. Tìm số chính phương   
        List<int> perfectSquares = list1.Where(x => IsPerfectSquare(x)).ToList();
        Console.WriteLine("SO CHINH PHUONG:");
        Console.WriteLine(string.Join(", ", perfectSquares));

        // 3. Tìm số nguyên tố
        List<int> primes = list1.Where(x => IsPrime(x)).ToList();
        Console.WriteLine("\nSO NGUYEN TO:");
        Console.WriteLine(string.Join(", ", primes));

        // 4. Clone list1 thành list2
        List<int> list2 = new List<int>(list1);

        // a. Sắp xếp tăng dần
        list2.Sort();
        Console.WriteLine("\nDANH SACH SAP XEP TANG DAN:");
        Console.WriteLine(string.Join(", ", list2));

        // b. Sắp xếp giảm dần
        list2.Reverse();
        Console.WriteLine("\nDANH SACH SAP XEP GIAM DAN:");
        Console.WriteLine(string.Join(", ", list2));

        // Ghi lại thời gian kết thúc
        DateTime endTime = DateTime.Now;

        // Tính tổng thời gian thực thi
        TimeSpan duration = endTime - startTime;
        Console.WriteLine($"\nTHOI GIAN THUC HIEN: {duration.TotalMilliseconds} ms");

        // Lưu list tăng dần vào txt
        string filePath = @"C:\PRN222\Bai1\Bai1\sorted_numbers.txt";
        File.WriteAllText(filePath, string.Join(", ", list2));
        Console.WriteLine($"Tệp đã được ghi thành công tại: {filePath}");

    }

    // Hàm kiểm tra số chính phương
    static bool IsPerfectSquare(int number)
    {
        int sqrt = (int)Math.Sqrt(number);
        return sqrt * sqrt == number;
    }

    // Hàm kiểm tra số nguyên tố
    static bool IsPrime(int number)
    {
        if (number <= 1) return false;
        if (number <= 3) return true;
        if (number % 2 == 0 || number % 3 == 0) return false;
        for (int i = 5; i * i <= number; i += 6)
        {
            if (number % i == 0 || number % (i + 2) == 0) return false;
        }
        return true;
    }
}

// viet unit tescase kiem tra 2 ham snt và so chinh phuong.
