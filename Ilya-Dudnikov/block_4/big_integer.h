#ifndef TASK_1_BIG_INTEGER_H
#define TASK_1_BIG_INTEGER_H

#define BASE 256
#define INT_SIZE 1000

#define TOO_BIG_FOR_DECIMAL -1

typedef struct big_integer
{
	int digits[INT_SIZE];
	int size;
} big_int;

/**
 * @brief Sets value of given big integer to 0
 * @param value - big integer
 */
void set_to_zero(big_int *value);

/**
 * Converts big integer to decimal, if possible
 * @param num
 * @return value of big integer in decimal, or -1 if the value is too big
 */
long long decimal(big_int *num);

/**
 * @brief Calculates sum of two big integers and saves it in left
 * @param left - big integer
 * @param right - big integer
 */
void big_int_add(big_int *left, big_int *right);

/**
 * @param left - big integer
 * @param right - big integer
 * @return product of two big integers
 */
big_int big_int_multiply(big_int *left, big_int *right);

#endif //TASK_1_BIG_INTEGER_H
