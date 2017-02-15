﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AssemblyHelperTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Reflection.Test
{
    using System;
    using System.IO;

    using FluentAssertions;

    using Xunit;

    /// <summary>
    /// Tests the <see cref="AssemblyHelper"/> class.
    /// </summary>
    public static class AssemblyHelperTest
    {
        private const string ExpectedTextFileContents = "this is an embedded text file";

        private const string EmbeddedTextFileName = "EmbeddedTextFile.txt";

        private const string EmbeddedGzipTextFileName = "EmbeddedTextFile.txt.gz";

        private const string EmbeddedIcoFileName = "EmbeddedIcon.ico";

        private static readonly string FullQualifiedEmbeddedTextFileName = typeof(AssemblyHelperTest).Namespace + "." + EmbeddedTextFileName;

        private static readonly string FullQualifiedEmbeddedGzipTextFileName = typeof(AssemblyHelperTest).Namespace + "." + EmbeddedGzipTextFileName;

        private static readonly string FullQualifiedEmbeddedIcoFileName = typeof(AssemblyHelperTest).Namespace + "." + EmbeddedIcoFileName;

        // ReSharper disable InconsistentNaming
        [Fact]
        public static void OpenEmbeddedResourceStream_without_assembly___Should_throw_ArgumentNullException___When_parameter_resourceName_is_null()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => AssemblyHelper.OpenEmbeddedResourceStream(null, false));
        }

        [Fact]
        public static void OpenEmbeddedResourceStream_without_assembly___Should_throw_ArgumentException___When_parameter_resourceName_is_white_space()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentException>(() => AssemblyHelper.OpenEmbeddedResourceStream(string.Empty, false));
            Assert.Throws<ArgumentException>(() => AssemblyHelper.OpenEmbeddedResourceStream("   ", false));
            Assert.Throws<ArgumentException>(() => AssemblyHelper.OpenEmbeddedResourceStream("  \r\n   ", false));
        }

        [Fact]
        public static void OpenEmbeddedResourceStream_without_assembly___Should_throw_InvalidOperationException___When_resource_does_not_exist()
        {
            // Arrange
            string thisNamespace = typeof(AssemblyHelperTest).Namespace;
            const string ResourceName1 = "NotThere";
            string resourceName2 = thisNamespace + "EmbeddedTextFileE.txt";

            // Act, Assert
            Assert.Throws<InvalidOperationException>(() => AssemblyHelper.OpenEmbeddedResourceStream(ResourceName1, false));
            Assert.Throws<InvalidOperationException>(() => AssemblyHelper.OpenEmbeddedResourceStream(resourceName2, false));
        }

        [Fact(Skip = "Not sure how to test this.")]
        public static void OpenEmbeddedResourceStream_without_assembly___Should_throw_InvalidOperationException___When_resource_is_not_an_embedded_resource()
        {
        }

        [Fact(Skip = "Not practical to test, would have to create a massive file.")]
        public static void OpenEmbeddedResourceStream_without_assembly___Should_throw_NotImplementedException___When_resource_length_is_greater_than_Int64_MaxValue()
        {
        }

        [Fact]
        public static void OpenEmbeddedResourceStream_without_assembly___Should_return_read_only_seekable_stream_of_the_embedded_resource___When_parameter_addCallerNamespace_is_false_and_embedded_resource_exists()
        {
            // Arrange, Act
            Stream actual = AssemblyHelper.OpenEmbeddedResourceStream(FullQualifiedEmbeddedTextFileName, false);

            // Assert
            Assert.True(actual.CanRead);
            Assert.True(actual.CanSeek);
            Assert.False(actual.CanWrite);
            Assert.False(actual.CanTimeout);
            using (var reader = new StreamReader(actual))
            {
                Assert.Equal(ExpectedTextFileContents, reader.ReadToEnd());
            }

            // Cleanup
            actual.Dispose();
        }

        [Fact]
        public static void OpenEmbeddedResourceStream_without_assembly___Should_return_read_only_seekable_stream_of_the_embedded_resource___When_parameter_addCallerNamespace_is_true_and_embedded_resource_exists()
        {
            // Arrange, Act
            Stream actual = AssemblyHelper.OpenEmbeddedResourceStream(EmbeddedTextFileName);

            // Assert
            Assert.True(actual.CanRead);
            Assert.True(actual.CanSeek);
            Assert.False(actual.CanWrite);
            Assert.False(actual.CanTimeout);
            using (var reader = new StreamReader(actual))
            {
                Assert.Equal(ExpectedTextFileContents, reader.ReadToEnd());
            }

            // Cleanup
            actual.Dispose();
        }

        [Fact]
        public static void OpenEmbeddedResourceStream_without_assembly___Should_return_read_only_seekable_stream_of_the_embedded_resource___When_resource_stream_is_already_open()
        {
            // Arrange, Act
            Stream actual;
            using (Stream priorOpenStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(FullQualifiedEmbeddedTextFileName))
            {
                // ReSharper disable once PossibleNullReferenceException
                priorOpenStream.Read(new byte[1], 0, 1);
                actual = AssemblyHelper.OpenEmbeddedResourceStream(FullQualifiedEmbeddedTextFileName, false);
                // ReSharper restore PossibleNullReferenceException
            }

            // Assert
            Assert.True(actual.CanRead);
            Assert.True(actual.CanSeek);
            Assert.False(actual.CanWrite);
            Assert.False(actual.CanTimeout);
            using (var reader = new StreamReader(actual))
            {
                Assert.Equal(ExpectedTextFileContents, reader.ReadToEnd());
            }

            // Cleanup
            actual.Dispose();
        }

        [Fact]
        public static void OpenEmbeddedResourceStream_without_assembly___Should_return_decompressed_read_only_stream_of_the_embedded_resource___When_parameter_decompressionMethod_is_Gzip_and_embedded_resource_was_compressed_using_gzip()
        {
            // Arrange, Act
            Stream actual = AssemblyHelper.OpenEmbeddedResourceStream(FullQualifiedEmbeddedGzipTextFileName, false, CompressionMethod.Gzip);

            // Assert
            actual.CanRead.Should().BeTrue();
            actual.CanSeek.Should().BeFalse();
            actual.CanWrite.Should().BeFalse();
            actual.CanTimeout.Should().BeFalse();
            using (var reader = new StreamReader(actual))
            {
                reader.ReadToEnd().Should().Be(ExpectedTextFileContents);
            }

            // Cleanup
            actual.Dispose();
        }

        [Fact]
        public static void OpenEmbeddedResourceStream_with_assembly___Should_throw_ArgumentNullException___When_parameter_assembly_is_null()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => AssemblyHelper.OpenEmbeddedResourceStream(null, EmbeddedTextFileName));
        }

        [Fact]
        public static void OpenEmbeddedResourceStream_with_assembly___Should_throw_ArgumentNullException___When_parameter_resourceName_is_null()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => System.Reflection.Assembly.GetExecutingAssembly().OpenEmbeddedResourceStream(null));
        }

        [Fact]
        public static void OpenEmbeddedResourceStream_with_assembly___Should_throw_ArgumentException___When_parameter_resourceName_is_white_space()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentException>(() => System.Reflection.Assembly.GetExecutingAssembly().OpenEmbeddedResourceStream(string.Empty));
            Assert.Throws<ArgumentException>(() => System.Reflection.Assembly.GetExecutingAssembly().OpenEmbeddedResourceStream("   "));
            Assert.Throws<ArgumentException>(() => System.Reflection.Assembly.GetExecutingAssembly().OpenEmbeddedResourceStream("  \r\n   "));
        }

        [Fact]
        public static void OpenEmbeddedResourceStream_with_assembly___Should_throw_InvalidOperationException___When_resource_does_not_exist()
        {
            // Arrange
            string thisNamespace = typeof(AssemblyHelperTest).Namespace;
            const string ResourceName1 = "NotThere";
            string resourceName2 = thisNamespace + "EmbeddedTextFileE.txt";

            // Act, Assert
            Assert.Throws<InvalidOperationException>(() => System.Reflection.Assembly.GetExecutingAssembly().OpenEmbeddedResourceStream(ResourceName1));
            Assert.Throws<InvalidOperationException>(() => System.Reflection.Assembly.GetExecutingAssembly().OpenEmbeddedResourceStream(resourceName2));
        }

        [Fact(Skip = "Not sure how to test this.")]
        public static void OpenEmbeddedResourceStream_with_assembly___Should_throw_InvalidOperationException___When_resource_is_not_an_embedded_resource()
        {
        }

        [Fact(Skip = "Not practical to test, would have to create a massive file.")]
        public static void OpenEmbeddedResourceStream_with_assembly___Should_throw_NotImplementedException___When_resource_length_is_greater_than_Int64_MaxValue()
        {
        }

        [Fact]
        public static void OpenEmbeddedResourceStream_with_assembly___Should_return_read_only_seekable_stream_of_the_embedded_resource___When_embedded_resource_exists()
        {
            // Arrange, Act
            Stream actual = System.Reflection.Assembly.GetExecutingAssembly().OpenEmbeddedResourceStream(FullQualifiedEmbeddedTextFileName);

            // Assert
            Assert.True(actual.CanRead);
            Assert.True(actual.CanSeek);
            Assert.False(actual.CanWrite);
            Assert.False(actual.CanTimeout);
            using (var reader = new StreamReader(actual))
            {
                Assert.Equal(ExpectedTextFileContents, reader.ReadToEnd());
            }

            // Cleanup
            actual.Dispose();
        }

        [Fact]
        public static void OpenEmbeddedResourceStream_with_assembly___Should_return_read_only_seekable_stream_of_the_embedded_resource___When_resource_stream_is_already_open()
        {
            // Arrange, Act
            Stream actual;
            using (Stream priorOpenStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(FullQualifiedEmbeddedTextFileName))
            {
                // ReSharper disable once PossibleNullReferenceException
                priorOpenStream.Read(new byte[1], 0, 1);
                actual = System.Reflection.Assembly.GetExecutingAssembly().OpenEmbeddedResourceStream(FullQualifiedEmbeddedTextFileName);
                // ReSharper restore PossibleNullReferenceException
            }

            // Assert
            Assert.True(actual.CanRead);
            Assert.True(actual.CanSeek);
            Assert.False(actual.CanWrite);
            Assert.False(actual.CanTimeout);
            using (var reader = new StreamReader(actual))
            {
                Assert.Equal(ExpectedTextFileContents, reader.ReadToEnd());
            }

            // Cleanup
            actual.Dispose();
        }

        [Fact]
        public static void OpenEmbeddedResourceStream_with_assembly___Should_return_decompressed_read_only_stream_of_the_embedded_resource___When_parameter_decompressionMethod_is_Gzip_and_embedded_resource_was_compressed_using_gzip()
        {
            // Arrange, Act
            Stream actual = System.Reflection.Assembly.GetExecutingAssembly().OpenEmbeddedResourceStream(FullQualifiedEmbeddedGzipTextFileName, CompressionMethod.Gzip);

            // Assert
            actual.CanRead.Should().BeTrue();
            actual.CanSeek.Should().BeFalse();
            actual.CanWrite.Should().BeFalse();
            actual.CanTimeout.Should().BeFalse();
            using (var reader = new StreamReader(actual))
            {
                reader.ReadToEnd().Should().Be(ExpectedTextFileContents);
            }

            // Cleanup
            actual.Dispose();
        }

        [Fact]
        public static void ReadEmbeddedResourceString___Should_throw_ArgumentNullException___When_parameter_resourceName_is_null()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentNullException>(() => AssemblyHelper.ReadEmbeddedResourceAsString(null, false));
        }

        [Fact]
        public static void ReadEmbeddedResourceString___Should_throw_ArgumentException___When_parameter_resourceName_is_white_space()
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentException>(() => AssemblyHelper.ReadEmbeddedResourceAsString(string.Empty, false));
            Assert.Throws<ArgumentException>(() => AssemblyHelper.ReadEmbeddedResourceAsString("   ", false));
            Assert.Throws<ArgumentException>(() => AssemblyHelper.ReadEmbeddedResourceAsString("  \r\n   ", false));
        }

        [Fact]
        public static void ReadEmbeddedResourceString___Should_throw_InvalidOperationException___When_resource_does_not_exist()
        {
            // Arrange
            string thisNamespace = typeof(AssemblyHelperTest).Namespace;
            const string ResourceName1 = "NotThere";
            string resourceName2 = thisNamespace + "EmbeddedTextFileE.txt";

            // Act, Assert
            Assert.Throws<InvalidOperationException>(() => AssemblyHelper.ReadEmbeddedResourceAsString(ResourceName1, false));
            Assert.Throws<InvalidOperationException>(() => AssemblyHelper.ReadEmbeddedResourceAsString(resourceName2, false));
        }

        [Fact(Skip = "Not sure how to test this.")]
        public static void ReadEmbeddedResourceString___Should_throw_InvalidOperationException___When_resource_is_not_an_embedded_resource()
        {
        }

        [Fact(Skip = "Not practical to test, would have to create a massive file.")]
        public static void ReadEmbeddedResourceString___Should_throw_NotImplementedException___When_resource_length_is_greater_than_Int64_MaxValue()
        {
        }

        [Fact]
        public static void ReadEmbeddedResourceString___Should_return_embedded_resource_as_string___When_parameter_addCallerNamespace_is_false_and_embedded_resource_exists()
        {
            // Arrange, Act
            string actual = AssemblyHelper.ReadEmbeddedResourceAsString(FullQualifiedEmbeddedTextFileName, false);

            // Assert
            Assert.Equal(ExpectedTextFileContents, actual);
        }

        [Fact]
        public static void ReadEmbeddedResourceString___Should_return_embedded_resource_as_string___When_parameter_addCallerNamespace_is_true_and_embedded_resource_exists()
        {
            // Arrange, Act
            string actual = AssemblyHelper.ReadEmbeddedResourceAsString(EmbeddedTextFileName);

            // Assert
            Assert.Equal(ExpectedTextFileContents, actual);
        }

        [Fact]
        public static void ReadEmbeddedResourceString___Should_return_embedded_resource_as_string___When_resource_stream_is_already_open()
        {
            // Arrange, Act
            string actual;
            using (Stream priorOpenStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(FullQualifiedEmbeddedTextFileName))
            {
                // ReSharper disable AssignNullToNotNullAttribute
                using (var reader = new StreamReader(priorOpenStream))
                {
                    reader.Read();
                    actual = AssemblyHelper.ReadEmbeddedResourceAsString(FullQualifiedEmbeddedTextFileName, false);
                }

                // ReSharper restore AssignNullToNotNullAttribute
            }

            // Assert
            Assert.Equal(ExpectedTextFileContents, actual);
        }

        [Fact]
        public static void ReadEmbeddedResourceString___Should_return_embedded_resource_as_string___When_parameter_addCallerNamespace_is_false_and_embedded_resource_is_not_text()
        {
            // Arrange, Act
            string actual = AssemblyHelper.ReadEmbeddedResourceAsString(FullQualifiedEmbeddedIcoFileName, false);

            // Assert
            Assert.NotEmpty(actual);
        }

        [Fact]
        public static void ReadEmbeddedResourceString___Should_return_embedded_resource_as_string___When_parameter_addCallerNamespace_is_true_and_embedded_resource_is_not_text()
        {
            // Arrange, Act
            string actual = AssemblyHelper.ReadEmbeddedResourceAsString(EmbeddedIcoFileName);

            // Assert
            Assert.NotEmpty(actual);
        }

        [Fact]
        public static void ReadEmbeddedResourceString___Should_throw_InvalidDataException___When_resource_has_not_been_compressed_using_gzip_and_parameter_decompressionMethod_is_Gzip()
        {
            // Arrange, Act
            var ex = Record.Exception(() => AssemblyHelper.ReadEmbeddedResourceAsString(EmbeddedTextFileName, decompressionMethod: CompressionMethod.Gzip));

            // Assert
            ex.Should().BeOfType<InvalidDataException>();
        }

        [Fact]
        public static void ReadEmbeddedResourceString___Should_return_string_decompressed_from_embedded_resource___When_resource_has_been_compressed_using_gzip_and_parameter_decompressionMethod_is_Gzip()
        {
            // Arrange, Act
            string actual = AssemblyHelper.ReadEmbeddedResourceAsString(EmbeddedGzipTextFileName, decompressionMethod: CompressionMethod.Gzip);

            // Assert
            actual.Should().Be(ExpectedTextFileContents);
        }

        // ReSharper restore InconsistentNaming
    }
}